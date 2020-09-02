import * as cdk from '@aws-cdk/core';
import * as iam from '@aws-cdk/aws-iam';
import * as kms from '@aws-cdk/aws-kms';
import * as sns from '@aws-cdk/aws-sns';
import * as codebuild from '@aws-cdk/aws-codebuild';
import * as codepipeline from '@aws-cdk/aws-codepipeline';
import * as codestarnotifications from '@aws-cdk/aws-codestarnotifications';
import * as codepipeline_actions from '@aws-cdk/aws-codepipeline-actions';
import * as codecommit from '@aws-cdk/aws-codecommit';
import { BuildProject } from './build-project';

import { ZeusServiceAccount, ZeusCorpAccount } from '@chessdb.biz/zeus-accounts';

const friendlyName = (name: string) : string => {
  if (name.length == 0) return name;
  if (name.length == 1) return name.toUpperCase();

  var first = name[0].toUpperCase();
  return first + name.substring(1, name.length);
}

interface CicdStackProps extends cdk.StackProps {
  deployAccount: ZeusCorpAccount,
  serviceAccounts: ZeusServiceAccount[]
}

export class CicdStack extends cdk.Stack {
  constructor(scope: cdk.Construct, id: string, props: CicdStackProps) {
    super(scope, id, {
      env: props.deployAccount.environment
    });

    const artifactKey = new kms.Key(this, 'ArtifactKey', {
      alias: 'eunomia-artifact-key',
      enableKeyRotation: true
    });

    const approvalTopicArn = cdk.Fn.importValue('PlumberApprovalsTopicArn');
    const notificationsTopicArn = cdk.Fn.importValue('PlumberNotificationsTopicArn');
    
    const herculesRepo = new codecommit.Repository(this, 'Repo', {
      repositoryName: 'eunomia'
    });

    const pipeline = new codepipeline.Pipeline(this, 'Pipeline', {
      pipelineName: 'Eunomia',
      
    });

    const sourceOutput = new codepipeline.Artifact('sourceOutput');
    const sourceStage = pipeline.addStage({
      stageName: 'Source'
    });
    sourceStage.addAction(new codepipeline_actions.CodeCommitSourceAction({
      repository: herculesRepo,
      actionName: 'Source',
      runOrder: 1,
      output: sourceOutput
    }));

    const buildProject = new BuildProject(this, 'BuildProject', {
      artifactKey: artifactKey
    });

    const buildOutput = new codepipeline.Artifact('buildOutput');
    const buildStage = pipeline.addStage({
      stageName: 'Build'
    });
    buildStage.addAction(new codepipeline_actions.CodeBuildAction({
      project: buildProject.project,
      input: sourceOutput,
      outputs: [
        buildOutput
      ],
      actionName: 'Build',
      runOrder: 1
    }));

    const deployRole = new iam.Role(this, 'DeployProjectRole', {
      assumedBy: new iam.CompositePrincipal(
        new iam.ServicePrincipal('codebuild.amazonaws.com'),
        new iam.ServicePrincipal('codepipeline.amazonaws.com')
      )
    });
    const deployAssumeRolePolicy = new iam.PolicyStatement();
    deployAssumeRolePolicy.addActions('sts:AssumeRole');
    deployAssumeRolePolicy.addAllResources();
    deployRole.addToPolicy(deployAssumeRolePolicy);

    const deployProject = new codebuild.PipelineProject(this, 'DeployProject', {
      role: deployRole,
      encryptionKey: artifactKey,
      buildSpec: codebuild.BuildSpec.fromSourceFilename('packages/infrastructure/build/buildspec.yml')
    });

    new codestarnotifications.CfnNotificationRule(this, 'Notifications', {
      name: 'HerculesNotifications',
      status: 'ENABLED',
      resource: pipeline.pipelineArn,
      targets: [
        {
          targetType: 'SNS',
          targetAddress: notificationsTopicArn
        }
      ],
      detailType: 'FULL',
      eventTypeIds: [
        'codepipeline-pipeline-action-execution-succeeded',
        'codepipeline-pipeline-action-execution-failed',
        'codepipeline-pipeline-action-execution-canceled',
        'codepipeline-pipeline-action-execution-started',
        'codepipeline-pipeline-stage-execution-started',
        'codepipeline-pipeline-stage-execution-succeeded',
        'codepipeline-pipeline-stage-execution-resumed',
        'codepipeline-pipeline-stage-execution-canceled',
        'codepipeline-pipeline-stage-execution-failed',
        'codepipeline-pipeline-pipeline-execution-failed',
        'codepipeline-pipeline-pipeline-execution-canceled',
        'codepipeline-pipeline-pipeline-execution-started',
        'codepipeline-pipeline-pipeline-execution-resumed',
        'codepipeline-pipeline-pipeline-execution-succeeded',
        'codepipeline-pipeline-pipeline-execution-superseded',
        'codepipeline-pipeline-manual-approval-failed',
        'codepipeline-pipeline-manual-approval-needed',
        'codepipeline-pipeline-manual-approval-succeeded'
      ]
    });
  }
}
