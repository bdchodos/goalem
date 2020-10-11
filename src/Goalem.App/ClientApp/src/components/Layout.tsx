import * as React from 'react';
import { Container } from 'reactstrap';
import NavMenu from './NavMenu';

export default (props: { isAuthenticated: boolean, children?: React.ReactNode }) => {
  return (
    <React.Fragment>
      <NavMenu isAuthenticated={props.isAuthenticated} />
      <Container>
          {props.children}
      </Container>
    </React.Fragment>
  );
};