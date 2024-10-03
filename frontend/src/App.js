import React, { Component } from "react";
import logo from "./logo.svg";
import "./App.css";

class App extends Component {
  constructor(props) {
    super(props);
    this.state = {
      errors: "",
    };
  }

  componentDidMount() {
    this.getListOfErrors();
  }

  getListOfErrors() {
    const initGetErrors = {
      method: "GET",
      headers: { "Conent-Type": "application/json" },
    };
    fetch(
      "https://2zrz7ja3z2g267h5lkiy37ommu0jaqlv.lambda-url.us-east-1.on.aws/",
      initGetErrors
    )
      .then((response) => response.json())
      .then((returnedResponse) => {
        this.setState({ errors: returnedResponse.Items });
      });
  }

  buildTableBody(errorList) {
    const errorRow = errorList.map((error) => {
      return (
        <tr className="row-highlight">
          <td>{error.MessageId.S}</td>
          <td>{error.LambdaName.S}</td>
          <td>{error.AlarmName.S}</td>
          <td>{error.InstanceId.S}</td>
          <td>{error.TimeStamp.S}</td>
        </tr>
      );
    });
    return (
      <table>
        <thead>
          <tr>
            <th>Message Id</th>
            <th>Lambda Name</th>
            <th>Alarm Name</th>
            <th>Instance Id</th>
            <th>Time Stamp</th>
          </tr>
        </thead>
        {errorRow}
      </table>
    );
  }

  render() {
    const listOfErros = this.state.errors;
    return (
      <div className="Application-Container">
        {listOfErros.length > 0 ? this.buildTableBody(listOfErros) : null}
      </div>
    );
  }
}

export default App;
