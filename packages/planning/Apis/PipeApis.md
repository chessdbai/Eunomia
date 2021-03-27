## CreatePipe

### Request Parameters

```
PUT /pipes/{pipeName}/

Content-Type: application/json

No request body
```

|Param Name|Location|Required|Description|
|---|---|---|---|
|pipeName|Path Param|Yes|The name of the pipe to create.|


### Possible Errors

|Error Type|HTTP Status Code|Scenario|
|---|---|---|
|DuplicatePipeException|409|A pipe already exists with the given name.|


## DescribePipe

### Request Parameters

```
GET /pipes/{pipeName}/
```

|Param Name|Location|Required|Description|
|---|---|---|---|
|pipeName|Path Param|Yes|The name of the pipe to describe.|


### Possible Errors

|Error Type|HTTP Status Code|Scenario|
|---|---|---|
|PipeNotFoundException|404|No pipe found with the given name.|

## DeletePipe

### Request Parameters

```
DELETE /pipes/{pipeName}/
```

|Param Name|Location|Required|Description|
|---|---|---|---|
|pipeName|Path Param|Yes|The name of the pipe to delete.|


### Possible Errors

|Error Type|HTTP Status Code|Scenario|
|---|---|---|
|PipeNotFoundException|404|No pipe found with the given name.|
|PipeLockedException|412|The specified pipe exists but is locked.|

## FlushPipe

### Request Parameters

```
DELETE /pipes/{pipeName}/
```

|Param Name|Location|Required|Description|
|---|---|---|---|
|pipeName|Path Param|Yes|The name of the pipe to delete.|


### Possible Errors

|Error Type|HTTP Status Code|Scenario|
|---|---|---|
|PipeNotFoundException|404|No pipe found with the given name.|
|PipeLockedException|412|The specified pipe exists but is locked.|