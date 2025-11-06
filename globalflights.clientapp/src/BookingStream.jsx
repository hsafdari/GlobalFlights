import React, { useState } from 'react';
import FlightList from './flightList';
const BookingStream = () => {
    const baseUrl = import.meta.env.VITE_API_BASE_URL;
    const [eventSource, setEventSource] = useState(null);
    const [flights,setFlights] = useState([]);
    //const outputRef = useRef(null);

    const connectToStream = (type) => {
        disconnect();
        const urlMap = {
            Bookings: `${baseUrl}/api/flight/stream`,
            Orders: `${baseUrl}/api/BookingStream/orders`,
            Combined: `${baseUrl}/api/BookingStream/combined`,
        };
        const source = new EventSource(urlMap[type]);
        setEventSource(source);
        setupEventListeners(source, type);
    };

    const setupEventListeners = (source, streamType) => {
        source.onopen = () => {
            addToOutput(`${streamType} stream connected`);
        };

        source.onmessage = (event) => {
           
            console.log(event);
            const data = JSON.parse(event.data);
            if (data.eventType === "finish") {
                disconnect();
            }
            if (data.eventType === "flights") {   
                console.log(data.data);
                setFlights(data.data);
            } else {
                addToOutput(`[${data.eventType}] ${JSON.stringify(data.data)}`);
            }
        };

        source.onerror = (event) => {
            addToOutput('Error occurred: ' + event);
        };
    };

    const disconnect = () => {
        if (eventSource) {
            eventSource.close();
            addToOutput('Disconnected from stream');
            setEventSource(null);
        }
    };

    const addToOutput = (message) => {
        const timestamp = new Date().toLocaleTimeString();
        //const output = outputRef.current;
        //if (output) {
        //    output.innerHTML += `[${timestamp}] ${message}<br>`;
        //    output.scrollTop = output.scrollHeight;
        //}
        console.log(`[${timestamp}] ${message}`);
    };
    return (
        <div>
            <h1>Booking Services SSE Stream</h1>
            <div>
                <button onClick={() => connectToStream('Bookings')}>Seach Booking</button>
                <button onClick={disconnect}>Disconnect</button>
            </div>
            <h2>Available Flights</h2>
            <FlightList flights={flights } />
        </div>        
    );
};

export default BookingStream;