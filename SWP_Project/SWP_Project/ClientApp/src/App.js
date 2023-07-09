
import React, { useState,useEffect } from "react";

import BeginPage from './beginlayout/BeginPage';
import Cookies from 'universal-cookie';
import LoginWithLocalStorage from "./Login/LoginWithLocalStorage";
import TaskApi from './Api/TaskApi';
import Test from './Api/Test';
const App = () => {
    const [loginStatus,updateLoginStatus]=React.useState(false)
    const [accountId, updateAccount] = useState('');
   
    const cookies = new Cookies();
    useEffect(() => {
        (async () => {
            const data = await fetch('https://localhost:7219/api/students/' + accountId)
                .then(res => res.json())
                .then(stu => {
                    cookies.set('myCat', stu, { path: '/' });
                })
        })()
    }, [accountId]);
    
    function setUpdateState(status) {
        updateLoginStatus(status)
    }



    return (
       //main return
       //<div className="App">
       //<BeginPage />
       // </div>



       //login return
       <div>
            {loginStatus ? <BeginPage updateLoginStatus={updateLoginStatus}/> :
                <LoginWithLocalStorage updateLoginStatus={updateLoginStatus} updateAccount={updateAccount} />
          }
       </div>


       //test return
        //<div>
        //    <Test/>
        //</div>

    )
}

export default App;
