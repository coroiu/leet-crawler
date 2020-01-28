# Leet-crawler
Heavily concurrent web crawler implemented in C# using the built in TPL.

## Issues
- The are a lot of edgecases that the `FileStorage` doesn't handle.
  - Query part of URI is usually the culprit.
  - URLs cannot be fully represented in windows file names leading to unsupported characters getting replaced by `_`, which in turn might lead to duplicate file names.
  - InMemory storage works a lot better in this regard.
  - Pages with urls without extensions currently get saved as `index.html` in their own separate folder.
    - This was necessary because tretton37 contains references to the same pages, but sometimes with extensions, and sometimes without, leading to race-conditions due to multiple threads trying to write to the same file.
- Poor (well, actually non-existent) error handling in `HttpBrowser`. Could probably parse aftonbladet if not for this. Well, that and the fact that the `FileStorage` fails.

## Future Improvements
- Algorithm is currently a DFS-style approach which doesn't really scale when the stack-size starts to grow.
- Maybe add control over the thread-pool and scheduling.
- Event-driven progress reporting instead of polling.
- Higher-level tests.
- More interactive GUI.
