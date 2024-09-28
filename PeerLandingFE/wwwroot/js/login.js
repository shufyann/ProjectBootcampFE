﻿async function submitLogin() {
    try {
        const email = document.getElementById('email').value;
        const password = document.getElementById('password').value;

        const response = await fetch('/ApiLogin/Login' ,
        {
            method : 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ email, password }),
        });

        const result = await response.json();
        console.log(result);

        if (response.ok) {
            localStorage.setItem('jwtToken', result.data.token);
            window.location.href = '/Home/Index';
        } else {
            alert(result.message || 'Login Failed. Please Try Again.');
        }
    }
    catch (error) {
        console.log(error);
        alert('Gagal:' + error.message);
    }
}