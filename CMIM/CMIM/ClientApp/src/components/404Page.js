import React from 'react';
import  { Redirect } from 'react-router-dom'

class Page404 extends React.Component {
 
render() {
  function OpenLink() {
    alert("ok");
  }
  return (
    <div id="notfound">
		<div className="notfound">
            <div className="notfound-404">
                <h1>4<span>0</span>4</h1>
            </div>
            <h2>the page you requested could not found</h2>
            <form className="notfound-search">
				<button id="btnBackHome" type="button">Revenir à l'acceuil</button>
			</form>
        </div>
	</div>
  );
}
}


export default Page404;