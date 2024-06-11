const API_URL = 'http://localhost:83/api/Home';

class dataservice {

    returnNumberandcolor() {
        return fetch(`${API_URL}/getnumberandcolor`);
    }

    returnPlayers() {
        return fetch(`${API_URL}/getplayers`);
    }

    returnPlayerByName(name) {
        return fetch(`${API_URL}/getplayerbyname/${name}`);
    }

    savePlayer(data) {
        return fetch(`${API_URL}/saveplayer`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            console.log('Success:', data);
            return data;
        })
        .catch((error) => {
            console.error('Error:', error);
            throw error;
        });
    }

  }
export default new dataservice()