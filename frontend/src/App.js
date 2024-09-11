import React, { Component } from "react";
import logo from "./logo.svg";
import "./App.css";

class App extends Component {
  constructor(props) {
    super(props);
  }

  render() {
    return (
      <div className="Application-Container">
        <table>
          <tr className="table-header-row">
            <p>Lambda Error Chart </p>
          </tr>
          <tr>
            <td className="text-style">MessageId</td>
            <td className="text-style">Alarm Name</td>
            <td className="text-style">InstanceId</td>
            <td className="text-style">Lambda Name</td>
            <td className="text-style">Time Of Occurence</td>
          </tr>
        </table>
      </div>
    );
  }
}

export default App;
