import React, { Component } from "react";
import "../App.css";

class LambdaErrorsComponent extends Component {
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
      mode: "cors",
      headers: {
        "Access-Control-Allow-Origin": "*",
        "Access-Control-Allow-Headers": "*",
      },
    };
    fetch(
      "https://cors-anywhere.herokuapp.com/https://qpo7kgwrahvf3jrkox63nd5zui0acrkz.lambda-url.us-east-1.on.aws/",
      initGetErrors
    )
      .then((response) => response.json())
      .then((returnedResponse) => {
        console.log(returnedResponse);
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
      <div className="Component-Container">
        {listOfErros.length > 0 ? this.buildTableBody(listOfErros) : null}
      </div>
    );
  }
}

export default LambdaErrorsComponent;
