
import Themeroutes from ".././routes/Router";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import FullLayout from ".././layouts/FullLayout";
import React, { useState, createContext, useEffect } from "react";
import SemesterOption from ".././conditionalpage/BeginChoose";
import HeaderOnly from '.././layouts/HeaderOnly';
import TaskApi from ".././Api/TaskApi";
import Cookies from 'universal-cookie';
export const AccountContext = createContext();


const BeginLayout = ({ children },props) => {
    const cookies = new Cookies();
    console.log('cookie')
    console.log(cookies.get('myCat')); 
    const [courseId, updateCourseId] = useState('');
    const [accountId, updateAccount] = useState('SE173508');
    const [studentProfile, updateStudentProfile] = React.useState([]);
    console.log(props.accountId)
    console.log(accountId)
    const handeClickCourse = (newCourse) => {
        updateCourseId(newCourse);
    }
    const resetCourse = () => {
        updateCourseId('');
    }
    const resetRole = () => {
        updateAccount('');
    }

    useEffect(() => {
   
        fetch('https://localhost:7219/api/students/' + accountId)
            .then(res => res.json())
            .then(stu => {
                updateStudentProfile(stu)
            })
    }, [])



    

    const value = {
        courseId, resetCourse, accountId, resetRole, studentProfile
    }
    return (

            <AccountContext.Provider value={value} >
                {!courseId ? (<HeaderOnly><SemesterOption course={courseId} handeClickCourse={handeClickCourse} /></HeaderOnly>) : (

                    <Routes>
                        {Themeroutes.map((route, index) => {
                            const Page = route.component;
                            const Layout = route.layout || FullLayout;
                            return (
                                <Route key={index}
                                    path={route.path}
                                    element={
                                        <Layout >
                                            
                                            <Page />
                                        </Layout>
                                    }
                                />
                            )
                        })}
                    </Routes>
                )}
            </AccountContext.Provider>
    
    );
};

export default BeginLayout;
