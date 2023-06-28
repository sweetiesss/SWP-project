import { Col, Row } from "reactstrap";
import ProjectChart from "../components/dashboard/ProjectChart";
import Feeds from "../components/dashboard/Feeds";
import ProjectTables from "../components/dashboard/ProjectTable";
import TopCards from "../components/dashboard/TopCards";
import { FaRegClock, FaRegPlayCircle, FaRegCheckCircle, FaListOl, FaRegTimesCircle } from 'react-icons/fa';

const Starter = () => {
  return (
    <div>
      {/***Top Cards***/}
      <Row>
              <Col sm="auto" lg="auto">
          <TopCards
                      bg="bg-warning text-light"
                      title="Queued"
                      subtitle="Queued Task"
                      number="12"
                      icon={<FaRegClock/>}
          />
        </Col>
              <Col sm="auto" lg="auto">
                  <TopCards
                      bg="bg-primary text-light"
                      title="Doing"
                      subtitle="Doing Task"
                      number="3"
                      icon={ <FaRegPlayCircle/>}
          />
        </Col>
              <Col sm="auto" lg="auto">
                  <TopCards
                      bg="bg-success text-light"
                      title="Complete"
                      subtitle="Completed Task"
                      number="16"
                      icon={<FaRegCheckCircle/>}
          />
        </Col>
              <Col sm="auto" lg="auto">
          <TopCards
                      bg="bg-secondary text-light"
                      title="Total"
                      subtitle="Total Task"
                      number="20"
                      icon={<FaListOl/> }
          />
              </Col>
              <Col sm="auto" lg="auto">
                  <TopCards
                      bg="bg-danger text-light"
                      title="Fail"
                      subtitle="Fail Tasks"
                      number="1"
                      icon={<FaRegTimesCircle/>}
                  />
              </Col>
      </Row>
      {/***Sales & Feed***/}
      <Row>
        <Col sm="6" lg="6" xl="7" xxl="8">
                 <ProjectChart />
        </Col>
        <Col sm="6" lg="6" xl="5" xxl="4">
          <Feeds />
        </Col>
      </Row>
      {/***Table ***/}
      <Row>
        <Col lg="12">
          <ProjectTables />
        </Col>
      </Row>

    </div>
  );
};

export default Starter;
