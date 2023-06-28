
import Themeroutes from "./routes/Router";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import FullLayout from "./layouts/FullLayout";
import { useState,createContext } from "react";
import SemesterOption from "./conditionalpage/SemesterOption";
import HeaderOnly from './layouts/HeaderOnly';


export const CourseContext = createContext();
console.log(createContext);

const App = () => {
   // return <SemesterOption />
    // return <div className="dark">{routing}</div>;
    const [courseId, updateCourseId] = useState('');

    const handeClickCourse = (newCourse) => {
        updateCourseId(newCourse);
    }
    const resetCourse = ()=>{
        updateCourseId('');
    }
    const value = {
        courseId, resetCourse
    }
   return (
       <div className="App">
           <CourseContext.Provider value={value}>
               {!courseId ? (<HeaderOnly><SemesterOption course={courseId} handeClickCourse={handeClickCourse}/></HeaderOnly>) : (
             
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
           </CourseContext.Provider>
        </div>
    )
};

export default App;
