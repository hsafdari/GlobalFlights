const flightView = ({ flight }) => {    
    return (
        <div style={styles.card}>
            <h3>{flight.origin} → {flight.destination}</h3>
            <p><strong>Departure:</strong> {flight.departureDate}</p>
            <p><strong>Return:</strong> {flight.returnDate}</p>
             <p><strong>Price:</strong> {flight.price.total} {flight.price.currency}</p>
            <a href={flight.Links} target="_blank" rel="noopener noreferrer">View Offer</a>
        </div>
    );
}
const styles = {
    card: {
        border: '1px solid #ddd',
        borderRadius: '8px',
        padding: '12px',
        marginBottom: '12px',
        boxShadow: '0 2px 4px rgba(0,0,0,0.1)',
        fontFamily: 'Arial, sans-serif',
        fontColor:"#FFF"
    }
};
export default flightView;