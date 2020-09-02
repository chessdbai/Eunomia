#!/bin/bash
set -euxo pipefail

cdk bootstrap aws://667342691845/us-east-2 --profile chessdb-deploy

npm run build
cdk deploy --profile chessdb-deploy EunomiaCicd --require-approval never