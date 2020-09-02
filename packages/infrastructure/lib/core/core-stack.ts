import * as cdk from '@aws-cdk/core';
import * as svcdiscovery from '@aws-cdk/aws-servicediscovery';

interface CoreStackProps extends cdk.StackProps {

}

export class CoreStack extends cdk.Stack {

  readonly serviceNamespace : svcdiscovery.IHttpNamespace;

  constructor(scope: cdk.Construct, id: string, props: CoreStackProps) {
    super(scope, id, props);

    svcdiscovery.HttpNamespace.fromHttpNamespaceAttributes
    const namespace = new svcdiscovery.HttpNamespace(this, 'StorageService', {
      name: 'eunomia',
    });
    
    const service1 = namespace.createService('', {
      description: 'service registering non-ip instances',
    });
    
    service1.registerNonIpInstance('NonIpInstance', {
      customAttributes: { arn: 'arn:aws:s3:::mybucket' },
    });
  }
}
