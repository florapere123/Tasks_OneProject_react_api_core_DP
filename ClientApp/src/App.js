import React, { Component } from 'react';
 
import { Provider } from 'react-redux'
 import './App.css'
import store from '../src/redux/store'
import './custom.css'
import TasksContainer from './containers/TasksContainer';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
        <Provider store={store}>
            <div className="App">
                <TasksContainer />
                </div>
            </Provider> 
    );
  }
}
