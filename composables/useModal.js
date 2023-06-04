export default () => {
  const requireLogin = (closable) => {
    const options = {
      closable: closable
    };
        
    const loginElement = document.getElementById('authentication-modal');
    if (!closable) {
        document.getElementById('close-login-modal-button').classList.add('hidden');
    }

    const loginModal = new Modal(loginElement, options);
    loginModal.show();
  };

  return {
    requireLogin
  };
}