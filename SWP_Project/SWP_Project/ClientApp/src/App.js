
import Themeroutes from "./routes/Router";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import FullLayout from "./layouts/FullLayout";
import React, { useState,createContext,useEffect } from "react";
import SemesterOption from "./conditionalpage/BeginChoose";
import HeaderOnly from './layouts/HeaderOnly';
import TaskApi from "./Api/TaskApi";
import { CookiesProvider } from "react-cookie";
import BeginPage from './beginlayout/BeginPage';

//export const AccountContext = createContext();

const App = () => {

    //const [courseId, updateCourseId] = useState('');
    //const [accountId, updateAccount] = useState(); 
    //const [studentProfile, updateStudentProfile] = React.useState([]);
    //const handeClickCourse = (newCourse) => {
    //    updateCourseId(newCourse);
    //}
    //const resetCourse = ()=>{
    //    updateCourseId('');
    //}
    //const resetRole = () => {
    //    updateAccount('');
    //}
    
    //useEffect(() => {
    //   console.log(accountId)
    //    fetch('https://localhost:7219/api/students/' + accountId)
    //        .then(res => res.json())
    //       .then(tasks => {
    //           updateStudentProfile(tasks)
    //       })
    //}, [])
  
    //console.log(studentProfile.studentTeams)


    //const value = {
    //    courseId, resetCourse, accountId, resetRole, studentProfile
    //}

   return (
       <div className="App">
           {/*<CookiesProvider>*/}
           {/*<AccountContext.Provider value={value} > */}
           {/*    {!courseId ? (<HeaderOnly><SemesterOption course={courseId} handeClickCourse={handeClickCourse}/></HeaderOnly>) : (*/}
             
           {/*    <Routes>*/}
           {/*        {Themeroutes.map((route, index) => {*/}
           {/*            const Page = route.component;*/}
           {/*            const Layout = route.layout || FullLayout;*/}
           {/*            return (*/}
           {/*                <Route key={index}*/}
           {/*                    path={route.path}*/}
           {/*                    element={*/}
           {/*                        <Layout >*/}
           {/*                            <Page />*/}
           {/*                        </Layout>*/}
           {/*                    }*/}
           {/*                />*/}
           {/*            )*/}
           {/*        })}*/}
           {/*        </Routes>*/}
           {/*    )}*/}
           {/*    </AccountContext.Provider>*/}
           {/*</CookiesProvider>*/}
           <BeginPage />
               {/*<TaskApi/>*/}
        </div>
    )
}

export default App;
