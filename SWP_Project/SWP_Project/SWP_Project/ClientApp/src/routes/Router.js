import { lazy } from "react";
import { Navigate } from "react-router-dom";
import HeaderOnly from "../layouts/HeaderOnly";




/****Layouts*****/
/*const FullLayout = lazy(() => import("../layouts/FullLayout.js"));*/

/***** Pages ****/
import Task from "../views/ui/Task";
import Starter from "../views/Starter";
import About from "../views/About";
import Report from "../views/ui/Report";
import TeamMember from "../views/ui/TeamMember";
import Profile from "../views/ui/Profile";

//const Task = lazy(() => import("../views/ui/Task"));
//const SemesterOptions = lazy(() => import("../views/ui/SemesterOption"));
//const Starter = lazy(() => import("../views/Starter"));

/*****Routes******/

const ThemeRoutes = [
  //{
  //  path: "/",
  //  element: <FullLayout />,
  //  children: [
      //  { path: "/", element: <Navigate to="/semesteroption" /> },
      //  {path:"/starter", exact:true,element:<Starter />},
      //{ path: "/task", exact: true, element: <Task /> },
      //{ path: "/semesteroption", exact: true, element: <SemesterOptions />,layout:"headerOnly" },
   
    { path: "/", component: Starter },
    { path: "/starter", component: Starter },
    { path: "/task", component: Task },
    { path: "/about", component: About },
    { path: "/report", component: Report },
    {path:"/teammember", component:TeamMember},
    { path: "/profile", component: Profile },

  //  ],
  //},
];

export default ThemeRoutes;
