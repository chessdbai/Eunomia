import * as cdk from '@aws-cdk/core';
import * as data from './data';

interface StorageStackProps extends cdk.StackProps {

}

export class StorageStack extends cdk.Stack {
  constructor(scope: cdk.Construct, id: string, props: StorageStackProps) {
    super(scope, id, props);

    const games = new data.GamesTable(this, 'Games', {
      
    });
  }
}
