window.addNavbarShadowOnScroll = (navbarId) => {
    const navbar = document.getElementById(navbarId);
    if (!navbar) return;

    window.addEventListener('scroll', () => {
        if (window.scrollY > 0) {
            navbar.classList.add('navbar-shadow');
        } else {
            navbar.classList.remove('navbar-shadow');
        }
    });
};