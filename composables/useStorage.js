export default() => {
  const clearSession = () => {
    sessionStorage.setItem('logged', false);
    sessionStorage.removeItem('token');
    sessionStorage.removeItem('username');
    sessionStorage.removeItem('email');
    sessionStorage.removeItem('profilePicture');
  };

  const clearLocal = () => {
    localStorage.setItem("logged", false);
    localStorage.removeItem("token");
    localStorage.removeItem("username");
    localStorage.removeItem("email");
    localStorage.removeItem("profilePicture");
  };

  return {
    clearSession,
    clearLocal 
  };
}
