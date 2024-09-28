async function fetchUsers() {
    try {
        const token = localStorage.getItem('jwtToken');
        const response = await fetch('/ApiMstUser/GetAllUsers', {
            method: 'GET',
            headers: {
                'Authorization': 'Bearer ' + token, // Tambahkan spasi di sini
            }
        });

        if (!response.ok) {
            alert('Failed to fetch users');
            return;
        }
        const res = await response.json();

        if (res.success) {
            populateUserTable(res.data);
        } else {
            alert(res.message || 'Login Failed. Please Try Again.');
        }
    } catch (error) {
        alert('No users found');
    }
}

function populateUserTable(users) {
    const userTableBody = document.querySelector('#userTable tbody');
    userTableBody.innerHTML = '';

    users.forEach(user => {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${user.name}</td>
            <td>${user.email}</td>
            <td>${user.role}</td>
            <td>${user.balance}</td>
            <td>
                <button class="btn btn-primary btn-sm" onclick="editUser('${user.id}')">Edit</button> 
                <button class="btn btn-danger btn-sm" onclick="deleteUser('${user.id}')">Delete</button>
            </td>
        `;
        userTableBody.appendChild(row);
    });
}

window.onload = fetchUsers();

function editUser(id) {
    const token = localStorage.getItem('jwtToken');

    fetch(`/ApiMstUser/GetUserById?id=${id}`, {
        method: 'GET',
        headers: {
            'Authorization': 'Bearer ' + token
        }
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Fail');
            }
            return response.json(); 
        })
        .then(data => {
            if (data.success) {
                const user = data.data;

                document.getElementById('userName').value = user.name;
                document.getElementById('userRole').value = user.role;
                document.getElementById('userBalance').value = user.balance;

                document.getElementById('userId').value = id;

                $('#editUserModal').modal('show'); 
            } else {
                alert('User not found');
            }
        })
        .catch(error => { 
            alert('Error: ' + error.message);
        });
}

async function updateUser() {
    const id = document.getElementById('userId').value;
    const name = document.getElementById('userName').value;
    const role = document.getElementById('userRole').value;
    const balance = document.getElementById('userBalance').value;

    const reqMstUserDto = {
        name: name,
        role: role,
        balance: parseFloat(balance)
    }
    console.log(reqMstUserDto)

    const token = localStorage.getItem('jwtToken');

    const response = await fetch(`ApiMstUser/UpdateUser/${id}`, {
        method: 'PUT',
        headers: {
            'Authorization': 'Bearer ' + token,
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(reqMstUserDto)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('fail');
            }
            return response.json();
        })
        .then(data => {
            alert('User success updated');
            $('#editUserModal').modal('hide');
            fetchUsers();
        })
        .catch(error => {
            alert('error: ' + error.message);
        });
}

function deleteUser(id) {
    const confirmation = confirm("Are you sure you want to delete this user?");

    if (!confirmation) {
        return;
    }

    const token = localStorage.getItem('jwtToken');

    fetch(`/ApiMstUser/DeleteUser/${id}`, {
        method: 'DELETE',
        headers: {
            'Authorization': 'Bearer ' + token
        }
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to delete user');
            }
            return response.text();
        })
        .then(message => {
            alert(message);
            fetchUsers();
        })
        .catch(error => {
            alert('Error deleting user: ' + error.message);
        });
}

function showAddUserModal() {
    function showAddUserModal() {
    console.log(document.getElementById('newUserName'));  // Log untuk melihat apakah elemen ditemukan
    $('#addUserModal').modal('show');
}

    $('#addUserModal').modal('show');

    const name = document.getElementById('newUserName').value;
    const email = document.getElementById('newUserEmail').value;
    const role = document.getElementById('newUserRole').value = 'lender';
   
}

function registerUser() {
    const name = document.getElementById('newUserName').value;
    const email = document.getElementById('newUserEmail').value;
    const role = document.getElementById('newUserRole').value;

    const reqAddMstUserDto = {
        name: name,
        email: email,
        password: 'password1',
        role: role,
        balance: 0
    };

    const token = localStorage.getItem('jwtToken');

    fetch(`/ApiMstUser/RegisterUser`, {
        method: 'POST',
        headers: {
            'Authorization': 'Bearer ' + token,
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(reqAddMstUserDto)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Cek data anda!');
            }
            return response.json();
        })
        .then(data => {
            alert('User added successfully!');
            $('#addUserModal').modal('hide');
            fetchUsers();
        })
        .catch(error => {
            alert('Error adding user: ' + error.message);
        });
}

function logoutUser() {
    localStorage.removeItem('jwtToken');
    window.location.href = '/login';
}
