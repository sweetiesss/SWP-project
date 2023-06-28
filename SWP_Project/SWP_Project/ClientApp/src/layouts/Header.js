import React, { useEffect,useContext } from "react";
import { Link } from "react-router-dom";

import {
    Badge,
    Navbar,
    Collapse,
    Nav,
    NavItem,
    NavbarBrand,
    UncontrolledDropdown,
    DropdownToggle,
    DropdownMenu,
    DropdownItem,
    Dropdown,
    Button,
    NavLink,
} from "reactstrap";

import { FaBell } from "react-icons/fa";
import user1 from "../assets/images/users/user1.jpg";
import { CourseContext } from "../App";

const Header = (props) => {
    const courseCon = useContext(CourseContext);
    const [isOpen, setIsOpen] = React.useState(false);
    const [dropdownOpen, setDropdownOpen] = React.useState(false);
    const [showTaskApi, updateTaskApi] = React.useState([]);
    useEffect(()=> { 
        fetch('https://localhost:7219/api/assignments')
            .then(res => res.json())
            .then(tasks => {
                updateTaskApi(tasks)
            })
    },[])
    const toggle = () => setDropdownOpen((prevState) => !prevState);
    const Handletoggle = () => {
        setIsOpen(!isOpen);
    };
    const showMobilemenu = () => {
        document.getElementById("sidebarArea").classList.toggle("showSidebar");
    };
    console.log(courseCon)
    return (
        <Navbar color="primary" dark expand="md">
            {/*<div className="d-flex align-items-center ">*/}
            {/*    <NavbarBrand href="/" className="d-lg-none">*/}
                   
            {/*    </NavbarBrand>*/}
            {/*    <Button*/}
            {/*        color="primary"*/}
            {/*        className="d-lg-none"*/}
            {/*        onClick={() => showMobilemenu()}*/}
            {/*    >*/}
            {/*        <i className="bi bi-list"></i>*/}
            {/*    </Button>*/}
            {/*</div>*/}
            {/*<div className="hstack gap-2">*/}
            {/*    <Button*/}
            {/*        color="primary"*/}
            {/*        size="sm"*/}
            {/*        className="d-sm-block d-md-none"*/}
            {/*        onClick={Handletoggle}*/}
            {/*    >*/}
            {/*        {isOpen ? (*/}
            {/*            <i className="bi bi-x"></i>*/}
            {/*        ) : (*/}
            {/*            <i className="bi bi-three-dots-vertical"></i>*/}
            {/*        )}*/}
            {/*    </Button>*/}
            {/*</div>*/}

            <Collapse navbar isOpen={isOpen}>
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
                            <DropdownToggle color="primary" isOpen={isOpen}>
                                <div className="position-relative d-inline-block"> <FaBell />{''}<Badge className="p-1 position-absolute translate-middle" color="danger" pill>4</Badge> </div>
                            </DropdownToggle>
                            <DropdownMenu>
                                {showTaskApi.map((task) => <DropdownItem key={task.id}>
                                    {task.name}
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
                                <DropdownItem>My Account</DropdownItem>
                                <DropdownItem>Edit Profile</DropdownItem>
                                <DropdownItem divider />
                                <DropdownItem>My Balance</DropdownItem>
                                <DropdownItem>Inbox</DropdownItem>
                                <DropdownItem>Logout</DropdownItem>
                            </DropdownMenu>
                        </Dropdown>
                    </div>
                </Nav>
            </Collapse>
        </Navbar>
    );
};

export default Header;
