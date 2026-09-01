// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Sidebar toggle
document.addEventListener('DOMContentLoaded', function () {
    var toggle = document.getElementById('sidebarToggle');
    var sidebar = document.getElementById('appSidebar');
    if (!toggle || !sidebar) return;

    // restore state
    var collapsed = localStorage.getItem('sidebarCollapsed') === '1';
    if (collapsed) sidebar.classList.add('collapsed');

    toggle.addEventListener('click', function () {
        sidebar.classList.toggle('collapsed');
        var now = sidebar.classList.contains('collapsed') ? '1' : '0';
        localStorage.setItem('sidebarCollapsed', now);
    });
});
