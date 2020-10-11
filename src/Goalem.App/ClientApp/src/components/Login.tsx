import * as React from 'react';
import { connect } from 'react-redux';
import LoginButton from './LoginButton';

const Login = () => (
  <div>
    <h1>Please Login</h1>
    <LoginButton />
  </div>
);

export default connect()(Login);