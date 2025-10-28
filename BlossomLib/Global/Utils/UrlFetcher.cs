using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

/// <summary> Comunicates to a Server through a URL by using HTTP Protocols. </summary>

public static class UrlFetcher
{
// Http Client used for Comunication

private static readonly HttpClient _client = new();

// Add new line

private static int AddLine(ReadOnlySpan<char> source, Span<char> target)
{
source.CopyTo(target);

string lineFeed = "\r\n";
lineFeed.CopyTo(target[source.Length..]);

return source.Length + lineFeed.Length;
}

// Build GET Request from URL as a String

public static NativeMemoryOwner<char> BuildGetRequest(string url)
{
using HttpRequestMessage request = new(HttpMethod.Get, url);

NativeMemoryOwner<char> rOwner = new(512);
var rawText = rOwner.AsSpan();

int pos = AddLine($"GET {request.RequestUri.PathAndQuery} HTTP/{request.Version.Major}.{request.Version.Minor}",
                   rawText[0..]);

var hostInfo = request.RequestUri.IsDefaultPort
? request.RequestUri.Host
: $"{request.RequestUri.Host}:{request.RequestUri.Port}";

pos += AddLine($"Host: {hostInfo}", rawText[pos..]);
pos += AddLine("", rawText[pos..]);

rOwner.Realloc( (ulong)pos);

return rOwner;
}

// Build GET Request from URL and write it to a Stream

public static void BuildGetRequest(string url, Stream writer)
{
using var rOwner = BuildGetRequest(url);

writer.WriteString(rOwner.AsSpan() );
}

// Build POST Request (Inner)

private static NativeMemoryOwner<char> BuildPostRequest(string url, HttpContent content)
{

using HttpRequestMessage request = new(HttpMethod.Post, url)
{
Content = content
};

NativeMemoryOwner<char> rOwner = new(1024);
var rawText = rOwner.AsSpan();

int pos = AddLine($"POST {request.RequestUri.PathAndQuery} HTTP/{request.Version.Major}.{request.Version.Minor}",
                  rawText[0..]);

var hostInfo = request.RequestUri.IsDefaultPort
? request.RequestUri.Host
: $"{request.RequestUri.Host}:{request.RequestUri.Port}";

pos += AddLine($"Host: {hostInfo}", rawText[pos..]);

foreach(var header in request.Headers)
{
var headVals = string.Join(", ", header.Value);

pos += AddLine($"{header.Key}: {headVals}", rawText[pos..]);
}

if(request.Content != null)
{
string body = request.Content.ReadAsStringAsync().Result;
var bodyLen = Encoding.UTF8.GetByteCount(body);

if(!request.Content.Headers.Contains("Content-Length") )
request.Content.Headers.ContentLength = bodyLen;

foreach(var contentHead in request.Content.Headers)
{
var cHeadVals = string.Join(", ", contentHead.Value);

pos += AddLine($"{contentHead.Key}: {cHeadVals}", rawText[pos..]);
}

pos += AddLine("", rawText[pos..]);
pos += AddLine(body, rawText[pos..]);
}

else
pos += AddLine("", rawText[pos..]);

rOwner.Realloc( (ulong)pos);

return rOwner;
}

// Build POST Request from URL as a String

public static NativeMemoryOwner<char> BuildPostRequest(string url, string content,
string contentType = "application/json")
{
using StringContent httpContent = new(content, Encoding.UTF8, contentType);

return BuildPostRequest(url, httpContent);
}

// Build POST Request from URL and write it to a Stream

public static void BuildPostRequest(string url, string content, Stream writer,
string contentType = "application/json")
{
using var rOwner = BuildPostRequest(url, content, contentType);

writer.WriteString(rOwner.AsSpan() );
}

// Build Query String from a HttpDoc

public static string BuildQuery<T>(string baseUrl, T doc) where T : HttpUrlDoc<T>
{
using ChunkedMemoryStream httpStream = new();

doc.WriteForm(httpStream);
httpStream.Seek(0, SeekOrigin.Begin);

using var hOwner = httpStream.ReadString();

return $"{baseUrl}?{hOwner}";
}

// Build Query String from a Dictionary

public static string BuildQuery(string baseUrl, Dictionary<string, string> parameters)
{

if(parameters == null || parameters.Count == 0)
return baseUrl;

List<string> queryParts = new();

foreach(var kvp in parameters)
{
string val = Uri.EscapeDataString(kvp.Value ?? string.Empty);

queryParts.Add($"{kvp.Key}={val}");
}

var queryString = string.Join("&", queryParts);

return $"{baseUrl}?{queryString}";
}


// Parse GET Request from Stream and get its base URL

public static string ParseGetRequest(Stream reader)
{
using var fOwner = reader.ReadLine();

if(fOwner is not NativeString flags)
return null;

var requestLine = flags.AsSpan();

int firstSpace = requestLine.IndexOf(' ');

if(firstSpace < 0)
return null;

int secondSpace = requestLine[(firstSpace + 1)..].IndexOf(' ');

if(secondSpace < 0)
return null;

var path = requestLine.Slice(firstSpace + 1, secondSpace);

NativeString? line;
string host = null;

while( (line = reader.ReadLine() ) != null)
{
using var vLine = line.Value;
ReadOnlySpan<char> rawLine = vLine.AsSpan().Trim();

if(rawLine.Length == 0)
break;

if(rawLine.StartsWith("Host:", StringComparison.OrdinalIgnoreCase) )
{
var hostInfo = rawLine[5..].Trim();

host = hostInfo.ToString();
break;
}

}

if(host == null)
return null;

return $"http://{host}{path.ToString()}";
}

// Parse POST Request from Stream and get its Body

public static NativeString ParsePostBody(Stream reader)
{
bool headersEnded = false;

long contentLength = -1;
NativeString? line;

while( (line = reader.ReadLine() ) != null)
{
using var vLine = line.Value;
ReadOnlySpan<char> header = vLine.AsSpan().Trim();

if(header.Length == 0)
{
headersEnded = true;
break;
}

string flags = "Content-Length:";

if(header.StartsWith(flags, StringComparison.OrdinalIgnoreCase) )
{
var contentInfo = header[flags.Length..].Trim();

contentLength = long.Parse(contentInfo);
}

}

if(!headersEnded || contentLength <= 0) 
return new();

NativeString bodyContent = new ( (ulong)contentLength);
long totalRead = 0;

while(totalRead < contentLength)
{
long toRead = contentLength - totalRead;

using var contentChunk = reader.ReadString(toRead);
var read = (int)contentChunk.Length;

if(read <= 0)
break;

var dest = bodyContent.AsSpan( (ulong)totalRead, read);
contentChunk.AsSpan().CopyTo(dest);

totalRead += read;
}

bodyContent.Realloc(totalRead);

return bodyContent;
}

// Parse Query String and Get Params as Dictionary

public static Dictionary<string, string> ParseQuery(string query)
{
Dictionary<string, string> result = new();

if(string.IsNullOrWhiteSpace(query) )
return result;

int paramsIndex = query.IndexOf('?');

string urlParams = paramsIndex >= 0 ? query[(paramsIndex + 1)..] : query;
var pairs = urlParams.Split('&', StringSplitOptions.RemoveEmptyEntries);

foreach(var pair in pairs)
{
var kv = pair.Split('=', 2);

string key = Uri.UnescapeDataString(kv[0]);
string val = kv.Length > 1 ? Uri.UnescapeDataString(kv[1]) : string.Empty;

result[key] = val;
}

return result;
}

// Parse Query and Get Params as HttpDoc

public static T ParseQuery<T>(string query) where T : HttpUrlDoc<T>, new()
{
using ChunkedMemoryStream httpStream = new();
T doc = new();

int paramsIndex = query.IndexOf('?');

if(paramsIndex == -1)
return null;

string urlParams = query[(paramsIndex + 1)..];

httpStream.WriteString(urlParams);
httpStream.Seek(0, SeekOrigin.Begin);

doc.ReadForm(httpStream);

return doc;
}

// Write HttpStatus to Logger

private static void LogHttpStatus(HttpStatusCode status)
{
string flags = status == default ? "Unknown" : status.ToString();

TraceLogger.WriteInfo($"Status: {(int)status} ({flags})");
}

// Get Response from Server

public static async Task<string> GetResponseAsync(string url)
{
HttpStatusCode status = default;
string responseBody = null;

TraceLogger.WriteActionStart($"Getting Response from {url}...");

try
{
using var response = await _client.GetAsync(url);

status = response.StatusCode;

if(status == HttpStatusCode.OK)
responseBody = await response.Content.ReadAsStringAsync();

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError($"Error getting response: {error.Message}");
}

LogHttpStatus(status);

return responseBody;
}

// Get Response from Server as a Stream

public static async Task<Stream> GetResponseStreamAsync(string url)
{
HttpStatusCode status = default;
ChunkedMemoryStream res = null;

TraceLogger.WriteActionStart($"Getting Response from {url}...");

try
{
using var response = await _client.GetAsync(url);

status = response.StatusCode;

if(status == HttpStatusCode.OK)
{
using var responseBody = await response.Content.ReadAsStreamAsync();
responseBody.Seek(0, SeekOrigin.Begin);

res = new();
FileManager.Process(responseBody, res);

res.Seek(0, SeekOrigin.Begin);
}

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError($"Error getting response: {error.Message}");
}

LogHttpStatus(status);

return res;
}

// Post Request to Server

public static async Task<string> PostRequestAsync(string url, string content,
string contentType = "application/json")
{
HttpStatusCode status = default;
string responseBody = null;

TraceLogger.WriteActionStart($"Posting Request to {url}...");

try
{
using StringContent httpContent = new(content, Encoding.UTF8, contentType);
using var response = await _client.PostAsync(url, httpContent);

status = response.StatusCode;

if(status == HttpStatusCode.OK)
responseBody = await response.Content.ReadAsStringAsync();

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError($"Error posting request: {error.Message}");
}

LogHttpStatus(status);

return responseBody;
}

// Post Request to Server as Stream

public static async Task<Stream> PostRequestStreamAsync(string url, Stream content,
string contentType = "application/json")
{
HttpStatusCode status = default;
ChunkedMemoryStream res = null;

TraceLogger.WriteActionStart($"Posting Request to {url}...");

try
{
using StreamContent httpContent = new(content);
httpContent.Headers.ContentType = new(contentType);

using var response = await _client.PostAsync(url, httpContent);

status = response.StatusCode;

if(status == HttpStatusCode.OK)
{
using var responseBody = await response.Content.ReadAsStreamAsync();
responseBody.Seek(0, SeekOrigin.Begin);

res = new();
FileManager.Process(responseBody, res);

res.Seek(0, SeekOrigin.Begin);
}

TraceLogger.WriteActionEnd();
}

catch(Exception error)
{
TraceLogger.WriteError($"Error posting request: {error.Message}");
}

LogHttpStatus(status);

return res;
}

// Download File Async

public static async Task DownloadFileAsync(string url, string filePath)
{
Stream responseStream = await GetResponseStreamAsync(url);

if(responseStream is null)
return;

using var outFile = FileManager.OpenWrite(filePath);
FileManager.Process(responseStream, outFile);

responseStream.Dispose();
}

// Upload File Async, and Optionally, Write Response in Local

public static async Task UploadFileAsync(string sourcePath, string url,
string contentType = "application/json", string responsePath = null)
{
using var inFile = FileManager.OpenRead(sourcePath);

Stream responseStream = await PostRequestStreamAsync(url, inFile, contentType);

if(responseStream is null)
return;

if(responsePath is not null)
{
using var outFile = FileManager.OpenWrite(responsePath);
FileManager.Process(responseStream, outFile);

responseStream.Dispose();
}

}

}