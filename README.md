# base64.net
base64 encode/decode in C# .net4.0.  If you don't want to use the builtin powershell cmdlet for some reason. 

## To make this source code into executeable

Make copy of this source code.  Always read source code from untrusted sources.
```
git clone https://github.com/studio-1b/base64.net.git
```

Then Find SDK compiler.  Visual studio 2010's is
```
C:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\IDE\devenv.com" /build Debug base64.net.sln
```
The SDK's is at
```
C:\Windows\Microsoft.NET\Framework\v3.5\MSBuild.exe base64.net.sln
```
And compile

## How to run

```
base64.net.exe
```
produces
```
Usage:  base64.net [file]
        type [file 1] | base64.net
Decode: base64.net --decode [file]
        type [file 1] | base64.net --decode

Advance: base64.net [file 1] ... [file n]
         concats all files, produces 1 base64 pack
         type [file] | base64.net [file 1] ... [file n]
         concats all files, produces 1 base64 pack
Decode:  base64.net --decode [file 1] ... [file n]
         decodes each file, then concat in output
         type [file] | base64.net --decode [file 1] ... [file n]
         decode stdin, then each files, then concat in output
```

Easy test, input, encodes, then decodes, produces original input:
```
echo "mary had little lamb" | base64.net.exe | base64.net.exe --decode
```
Produces
```
"mary had little lamb"
```



## This functionality is built into Powershell (free in windows)

This utility, is just for people who don't want to use Powershell, but want base64.

See blog below:
https://pavolkutaj.medium.com/how-to-encode-a-file-contents-into-base64-with-powershell-983d76a093cf

You can enter this directly in Powershell and get the Base64 encoding of any string:
```
[System.Convert]::ToBase64String([System.Text.Encoding]::UTF8.GetBytes("ssdfkf"))
```
Result base 64 encoding for "ssdfkf" is:
<pre>
c3NkZmtm
</pre>
