import React, { Component } from "react";
import logo from "./logo.svg";
import "./App.css";

class App extends Component {
  constructor(props) {
    super(props);
    this.state = {
      errors: ''
    }
  }

  render() {
    return (
      <div className="Application-Container">    
        <table>
          <thead>
              <tr>
                  <th>MessageId</th>
                  <th>Lambda Name</th>
                  <th>Alarm Name</th>
                  <th>Instance Id</th>
                  <th>Time Stamp</th>
              </tr>
            </thead>
            <tr className="row-highlight">
                <td>James</td>
                <td>Chicago</td>
                <td>Check</td>
                <td>Check</td>
                <td>Check</td>
            </tr>
            <tr className="row-highlight">
                <td>Robert</td>
                <td>New York</td>
                <td>Check</td>
                <td>Check</td>
                <td>Check</td>
            </tr>
        </table>
      </div>
    );
  }
}

export default App;
