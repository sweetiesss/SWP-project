import React, { useEffect,useContext } from "react";
import { Link } from "react-router-dom";

import {
    Badge,
    Navbar,
    Collapse,
    Nav,
    NavItem,
    UncontrolledDropdown,
    DropdownToggle,
    DropdownMenu,
    DropdownItem,
    Dropdown,
    NavLink,
} from "reactstrap";

import { FaBell } from "react-icons/fa";
import user1 from "../assets/images/users/user1.jpg";
//import { AccountContext } from "../App";
import { AccountContext } from "../beginlayout/BeginPage";
const Header = (props) => {
    const courseCon = useContext(AccountContext);
    const [open, setIsOpen] = React.useState(false);
    const [dropdownOpen, setDropdownOpen] = React.useState(false);
    const [showTaskApi, updateTaskApi] = React.useState([]);
    useEffect(() => {
        fetch('https:localhost:7219/api/students/' + courseCon.accountId )
                .then(res => res.json())
                .then(tasks => {
                    updateTaskApi(tasks.assignmentStudents)
                })
    }, [])

     //})()

    const toggle = () => setDropdownOpen((prevState) => !prevState);
    //console.log(showTaskApi)
    return (
        <Navbar color="primary" dark expand="md">
            <Collapse navbar isOpen={open}>
                <Nav className="me-auto" navbar>
                    <NavItem>
                        <Link to="/" className="nav-link">
                        <h5>Group 02</h5>
                        </Link>
                    </NavItem>
                    <NavItem>
                        <Link to="/" className="nav-link">
                            Home
                        </Link>
                    </NavItem>
                    <NavItem>
                        <Link to="/about" className="nav-link">
                            About
                        </Link>
                    </NavItem>
                    <UncontrolledDropdown inNavbar nav>
                        <DropdownToggle caret nav>
                            DD Menu
                        </DropdownToggle>
                        <DropdownMenu end>
                            <DropdownItem>Option 1</DropdownItem>
                            <DropdownItem>Option 2</DropdownItem>
                            <DropdownItem divider />
                            <DropdownItem>Reset</DropdownItem>
                        </DropdownMenu>
                    </UncontrolledDropdown>
                    {courseCon.courseId?( 
                    <NavItem>
                            <NavLink onClick={courseCon.resetCourse} style={{ cursor:"pointer" } }>
                        {courseCon.courseId}
                        </NavLink>
                    </NavItem>):<></>}
                </Nav>
                <Nav navbar>
                    <div className="d-flex align-items-center">
                        <UncontrolledDropdown >
                            <DropdownToggle color="primary" isOpen={open}>
                                <div className="position-relative d-inline-block">
                                    <FaBell />{''}<Badge className="p-1 position-absolute translate-middle" color="danger" pill>
                                        4
                                    </Badge> </div>
                            </DropdownToggle>
                            <DropdownMenu>
                                {showTaskApi.map((task) => <DropdownItem key={task.id}>
                                    {task.taskId}
                                </DropdownItem>)}
                            </DropdownMenu>
                        </UncontrolledDropdown>
                        <Dropdown isOpen={dropdownOpen} toggle={toggle}>
                            <DropdownToggle color="primary">
                                <img
                                    src={user1}
                                    alt="profile"
                                    className="rounded-circle"
                                    width="30"
                                ></img>
                            </DropdownToggle>
                            <DropdownMenu>
                                <DropdownItem header>Info</DropdownItem>
                                <DropdownItem>   <Link to="/profile" className="nav-link" style={{color:'black'} }>
                                    Profile
                                </Link></DropdownItem>
                                <DropdownItem divider />
                                <DropdownItem> <Link to="/" className="nav-link" style={{ color: 'black' }}>
                                    Logout
                                </Link></DropdownItem>
                            </DropdownMenu>
                        </Dropdown>
                    </div>
                </Nav>
            </Collapse>
        </Navbar>
    );
};

export default Header;
