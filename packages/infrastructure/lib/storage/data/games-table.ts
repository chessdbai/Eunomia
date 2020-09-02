import * as cdk from '@aws-cdk/core';
import * as ddb from '@aws-cdk/aws-dynamodb';
import * as iam from '@aws-cdk/aws-iam';
import * as kms from '@aws-cdk/aws-kms';

interface GamesTableProps {

}

export class GamesTable extends cdk.Stack {

  readonly key : kms.IKey;
  readonly table : ddb.ITable;

  constructor(scope: cdk.Construct, id: string, props: GamesTableProps) {
    super(scope, id, props);

    const encryptionKey = new kms.Key(this, 'Key', {
      alias: 'games-table'
    });

    const table = new ddb.Table(this, 'GamesTable', {
      encryption: ddb.TableEncryption.AWS_MANAGED,
      encryptionKey: encryptionKey,
      pointInTimeRecovery: true,
      stream: ddb.StreamViewType.NEW_IMAGE,
      partitionKey: {
        type: ddb.AttributeType.STRING,
        name: 'UniqueId',
      }
    });

    const tableTags = cdk.Tags.of(table);
    tableTags.add("Aspect", "GameStorage");

    this.key = encryptionKey;
    this.table = table;
  }
}