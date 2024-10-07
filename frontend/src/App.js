import React, { Component } from "react";
import parseRoute from "./lib/parseRoute";
import LambdaErrorsComponent from "./components/LambdaErrorsComponent";
import Sidebar from "./components/sidebar";
import Home from "./components/home";
import "./App.css";

class App extends Component {
  constructor(props) {
    super(props);
    this.state = {
      component: "LambdaErrorsComponent",
    };
  }

  componentDidMount() {
    window.addEventListener("hashchange", () => {
      const activeRoute = parseRoute(window.location.hash).path;
      this.setState({ component: activeRoute });
    });
  }

  renderComponent = () => {
    switch (this.state.component) {
      case "LambdaErrorsComponent":
        return <LambdaErrorsComponent />;
      default:
        return <Home />;
    }
  };

  render() {
    console.log(window.location.hash);
    return (
      <React.Fragment>
        <Sidebar />
        <div className="components">{this.renderComponent()}</div>
      </React.Fragment>
    );
  }
}

export default App;
