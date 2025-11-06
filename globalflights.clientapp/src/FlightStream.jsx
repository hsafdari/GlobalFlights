import React, { useEffect, useState } from "react";
import FlightView from "./flightView";
export default function FlightStream() {    
    const [flights, setFlights] = useState([]);
    const [isConnected, setIsConnected] = useState(false);
    const baseUrl = import.meta.env.VITE_API_BASE_URL;
    let eventSource;
    const clickEvent = () => {
        eventSource = new EventSource(`${baseUrl}/api/flight/stream`);
        //eventSource.close();
        // Connect to SSE endpoint
        eventSource.onopen = () => {
            console.log("SSE connected" + "//n");
            setIsConnected(true);
        };
        setupEventListeners();
    }
    useEffect(() => {
        console.log("flights updated:", flights);
    }, [flights]);
    const setupEventListeners = () => {
        eventSource.addEventListener("message", (e) => {
            if (!e.data) return;
            try {
                const item = JSON.parse(e.data);
                if (item.eventType === "finish") {
                    eventSource.close();
                }
                if (item.data.message != "") {
                    const messageItem = item?.data?.message;
                    console.log("messageItem", messageItem);                   
                    setFlights((prev) => [...prev, messageItem])
                }
            } catch (ex) {
                console.warn(ex);
                //console.warn("Non-JSON SSE message:", e.data);
            }
        });
        eventSource.onerror = (err) => {
            console.error("SSE error:", err);
            eventSource.close();
            setIsConnected(false);
        };
    }
    return (
        <div className="p-6">
            <h2 className="text-xl font-bold mb-4">
                ✈️ Live Flight Search Stream
            </h2>
            <div className="mb-3 text-gray-600">
                Status:{" "}
                <span className={isConnected ? "text-green-600" : "text-red-600"}>
                    {isConnected ? "Connected" : "Disconnected"}
                </span>
            </div>
            <div className="mb-3">
                <button className="btn" onClick={() => clickEvent()}>search flights</button>
            </div>
            <div>
                {flights.map((msgItem, msgIndex) => (
                    <div key={msgIndex}>
                        <h5>flights from:{msgItem.providerCode}</h5>  
                        <div>
                            {msgItem.data.map((item, index) => (
                                <div className="text-sm text-gray-700">
                                    <FlightView key={index} flight={item} />
                                </div>
                            ))}
                        </div>
                        <div>Response Count:{msgItem.meta.count}</div>
                        <div>Response Link:{msgItem.meta.links.self}</div>
                        <div>Response Provider: {msgItem.providerCode}</div>
                    </div>
                ))}
            </div>
       </div>
    );
}