import FlightView from './flightView'
const flightList = ({ flights }) => {
    <div>
        {flights.map((flight, index) => (
            <FlightView key={index} flight={flight} />
        ))}
    </div>
}
export default flightList;