#!/usr/bin/env node
import 'source-map-support/register';
import * as cdk from '@aws-cdk/core';
import * as stacks from '../lib';
import AccountManager, { ZeusServiceAccount, ZeusCorpAccount } from '@chessdb.biz/zeus-accounts';

const app = new cdk.App();

const deployAccount : ZeusCorpAccount = AccountManager.getAccounts({
  tag: 'Deployment'
})[0] as ZeusCorpAccount;
const serviceAccounts : ZeusServiceAccount[] =
  AccountManager.getAccounts({
    tag: 'Service'
  }).map(acc => (acc as ZeusServiceAccount));

new stacks.CicdStack(app, 'EunomiaCicd', {
  deployAccount: deployAccount,
  serviceAccounts: serviceAccounts
});