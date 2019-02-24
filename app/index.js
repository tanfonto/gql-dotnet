import React from 'react';
import ReactDOM from 'react-dom';
import GraphiQL from 'graphiql';
import styles from '../node_modules/graphiql/graphiql.css'
import fetch from 'isomorphic-fetch';
 
function graphQLFetcher(graphQLParams) {
  return fetch(window.location.origin + '/api', {
    method: 'post',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(graphQLParams),
  }).then(response => response.json());
}
 
ReactDOM.render(<GraphiQL fetcher={graphQLFetcher} />, document.getElementById('app'));