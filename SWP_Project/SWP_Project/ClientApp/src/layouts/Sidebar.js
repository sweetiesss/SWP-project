import { Button, Nav, NavItem } from "reactstrap";
import Logo from "./Logo";
import { Link, useLocation } from "react-router-dom";

const navigation = [
  {
    title: "Dashboard",
    href: "/starter",
    icon: "bi bi-speedometer2",
  },
  {
    title: "Task",
    href: "/task",
    icon: "bi bi-bell",
  },
  {
    title: "Report",
   href: "/report",
    icon: "bi bi-patch-check",
  },
  {
    title: "TeamMember",
    href: "/teammember",
    icon: "bi bi-hdd-stack",
  },
  //{
  //  title: "Cards",
  //  href: "/cards",
  //  icon: "bi bi-card-text",
  //},
  //{
  //  title: "Grid",
  //  href: "/grid",
  //  icon: "bi bi-columns",
  //},
  //{
  //  title: "Table",
  //  href: "/table",
  //  icon: "bi bi-layout-split",
  //},
  //{
  //  title: "Forms",
  //  href: "/forms",
  //  icon: "bi bi-textarea-resize",
  //},
  //{
  //  title: "Breadcrumbs",
  //  href: "/breadcrumbs",
  //  icon: "bi bi-link",
  //},
  //{
  //  title: "About",
  //  href: "/about",
  //  icon: "bi bi-people",
  //},
];

const Sidebar = () => {
  const showMobilemenu = () => {
    document.getElementById("sidebarArea").classList.toggle("showSidebar");
  };
  let location = useLocation();

  return (
    <div className="p-3">
          <div className="position-absolute  end-0  translate-middle">
     
        <span className="ms-auto d-lg-none">
        <Button
          close
          size="sm"
          className="ms-auto d-lg-none"
          onClick={() => showMobilemenu()}
        ></Button>
        </span>
      </div>
      <div className="pt-0 mt-0">
        <Nav vertical className="sidebarNav">
          {navigation.map((navi, index) => (
            <NavItem key={index} className="sidenav-bg">
              <Link
                to={navi.href}
                className={
                  location.pathname === navi.href
                    ? "text-primary nav-link py-3"
                    : "nav-link text-secondary py-3"
                }
              >
                <i className={navi.icon}></i>
                <span className="ms-3 d-inline-block">{navi.title}</span>
              </Link>
            </NavItem>
          ))}
        </Nav>
      </div>
    </div>
  );
};

export default Sidebar;
