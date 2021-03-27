## DescribeJob

### Request Parameters

```
GET /jobs/{jobId}/
```

|Param Name|Location|Required|Description|
|---|---|---|---|
|jobId|Path Param|Yes|The id of the job to describe.|


### Possible Errors

|Error Type|HTTP Status Code|Scenario|
|---|---|---|
|PipeNotFoundException|404|No job found with the given name.|
