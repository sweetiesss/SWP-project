import { Card, CardBody, CardSubtitle, CardTitle } from "reactstrap";
import Chart from "react-apexcharts";
import { useState } from 'react';

const ProjectChart = (props) => {
    
        const chartOptions= {
            optionsMixedChart: {
                chart: {
                    id: "basic-bar",
                    toolbar: {
                        show: false
                    }
          
                },
                plotOptions: {
                    bar: {
                        columnWidth: "70%",
                        
                    }
                },
                stroke: {
                    width: [4, 0, 0]
                },
                xaxis: {
                    categories: ["Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"],

                },
                markers: {
                    size: 6,
                    strokeWidth:3,
                    fillOpacity: 0,
                    strokeOpacity: 0,
                    hover: {
                        size: 8
                    },
    
                },
                
                yaxis: {
                    tickAmount: 10,
                    min: 0,
                    max: 10,
 
                },
                legend: {
                    horizontalAlign: "left",
                    offsetX: 10
                }
            },
            seriesMixedChart: [
                {
                    name: "Progess",
                    type: "line",
                    data: [ 2.4, 3.4, 2.6, 5.4, 3.8, 3, 3.2]
                },
                {
                    name: "Huy Bao",
                    type: "column",
                    data: [ 2,2, 4, 7, 8, 1, 2]
                },
                {
                    name: "Gia Huy",
                    type: "column",
                    data: [ 1,5, 4, 7, 4, 3, 3]
                },
                {
                    name: "Minh Nhat",
                    type: "column",
                    data: [ 3, 1, 2, 6, 1, 3, 8]
                },
                {
                    name: "Duy Lam",
                    type: "column",
                    data: [4, 1, 2, 6, 5, 7, 2]
                },
                {
                    name: "Tuan Minh",
                    type: "column",
                    data: [ 5, 7, 1, 1, 1, 3, 1]
                }
            ],
            optionsRadial: {
                plotOptions: {
                    radialBar: {
                        startAngle: -135,
                        endAngle: 225,
                        hollow: {
                            margin: 0,
                            size: "70%",
                            background: "#fff",
                            image: undefined,
                            imageOffsetX: 0,
                            imageOffsetY: 0,
                            position: "front",
                            dropShadow: {
                                enabled: true,
                                top: 3,
                                left: 0,
                                blur: 4,
                                opacity: 0.24
                            }
                        },
                        track: {
                            background: "#fff",
                            strokeWidth: "67%",
                            margin: 0, // margin is in pixels
                            dropShadow: {
                                enabled: true,
                                top: -3,
                                left: 0,
                                blur: 4,
                                opacity: 0.35
                            }
                        },

                        dataLabels: {
                            showOn: "always",
                            name: {
                                offsetY: -20,
                                show: true,
                                color: "#888",
                                fontSize: "13px"
                            },
                            value: {
                                formatter: function (val) {
                                    return val;
                                },
                                color: "#111",
                                fontSize: "30px",
                                show: true
                            }
                        }
                    }
                },
                fill: {
                    type: "gradient",
                    gradient: {
                        shade: "dark",
                        type: "horizontal",
                        shadeIntensity: 0.5,
                        gradientToColors: ["#ABE5A1"],
                        inverseColors: true,
                        opacityFrom: 1,
                        opacityTo: 1,
                        stops: [0, 100]
                    }
                },
                stroke: {
                    lineCap: "round"
                },
                labels: ["Percent"]
            },
            seriesRadial: [46],
            optionsBar: {
                chart: {
                    stacked: true,
                    stackType: "100%",
                    toolbar: {
                        show: false
                    }
                },
                plotOptions: {
                    bar: {
                        horizontal: true
                    }
                },
                dataLabels: {
                    dropShadow: {
                        enabled: true
                    }
                },
                stroke: {
                    width: 0
                },
                xaxis: {
                    categories: ["Fav Color"],
                    labels: {
                        show: false
                    },
                    axisBorder: {
                        show: false
                    },
                    axisTicks: {
                        show: false
                    }
                },
                fill: {
                    opacity: 1,
                    type: "gradient",
                    gradient: {
                        shade: "dark",
                        type: "vertical",
                        shadeIntensity: 0.35,
                        gradientToColors: undefined,
                        inverseColors: false,
                        opacityFrom: 0.85,
                        opacityTo: 0.85,
                        stops: [90, 0, 100]
                    }
                },

                legend: {
                    position: "bottom",
                    horizontalAlign: "right"
                }
            },
            seriesBar: [
                {
                    name: "blue",
                    data: [32]
                },
                {
                    name: "green",
                    data: [41]
                },
                {
                    name: "yellow",
                    data: [12]
                },
                {
                    name: "red",
                    data: [65]
                }
            ]
        };
        return (
            <div className="app">
                <div className="row">
                    <div className="col mixed-chart">
                        <Chart
                            options={chartOptions.optionsMixedChart}
                            series={chartOptions.seriesMixedChart}
                            type="line"
                            width="500"
                        />
                    </div>

                    <div className="col radial-chart">
                        <Chart
                            options={chartOptions.optionsRadial}
                            series={chartOptions.seriesRadial}
                            type="radialBar"
                            width="300"
                        />
                    </div>
                </div>

                {/*<div className="row">*/}
                {/*    <div className="col percentage-chart">*/}
                {/*        <Chart*/}
                {/*            options={chartOptions.optionsBar}*/}
                {/*            height={140}*/}
                {/*            series={chartOptions.seriesBar}*/}
                {/*            type="bar"*/}
                {/*            width={500}*/}
                {/*        />*/}
                {/*    </div>*/}
                {/*</div>*/}
            </div>
        );
};

export default ProjectChart;
