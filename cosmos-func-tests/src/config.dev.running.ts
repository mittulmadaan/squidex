import { buildConfig } from './config';

// Config to run protractor locally with an already running instance of Cosmos
export const config = buildConfig({ url: 'http://localhost:5000', setup: false });