## IndexGame

### Request Parameters

```
PUT /games/{collectionName}/

Content-Type: application/x-chess-pgn


...PGN game as request body
```

|Param Name|Location|Required|Description|
|---|---|---|---|
|collectionName|Path Param|Yes|The collection to make the game a part of.|
|X-ChessDB-Pipe|Header|No|The pipe name to index the game into.|

### Possible Errors

|Error Type|HTTP Status Code|Scenario|
|---|---|---|
|InvalidPgnGameException|400|PGN text could not be parsed by the Eunomia PGN parser.|
|CollectionNotFoundException|404|The specified collection does not exist.|
|PipeNotFoundException|404|If the pipe parameter was specified, the pipe requested does not exist.|
|PipeLockedException|412|If the pipe parameter was specified, the pipe requested is currently locked.|
