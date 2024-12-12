document.addEventListener("keydown", function (event) {
    const directions = {
        "ArrowUp": "Top",
        "z": "Top",
        "ArrowDown": "BOTTOM",
        "s": "BOTTOM",
        "ArrowLeft": "LEFT",
        "q": "LEFT",
        "ArrowRight": "RIGHT",
        "d": "RIGHT"
    };

    const direction = directions[event.key];

    if (!direction) return;

    $.ajax({
        url: `/Labyrinthe/MovePlayer/Player1/${direction}`,
        type: 'POST',
        success: function () {
            console.log("Direction envoyée :", direction);
            location.reload();
        },
        error: function () {
            console.error("Erreur lors du déplacement du joueur.");
        }
    });

});