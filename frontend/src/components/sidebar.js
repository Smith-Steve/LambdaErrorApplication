import React from "react";

export default function Sidebar() {
  return (
    <React.Fragment>
      <div className="sidebar">
        <a className="active" href="#home">
          Home
        </a>
        <a href="#LambdaErrorsComponent">Lambda Errors</a>
        <a href="#contact">Health Dashboard</a>
      </div>
    </React.Fragment>
  );
}
