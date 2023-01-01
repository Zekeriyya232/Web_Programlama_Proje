$(document).ready(function () {
    var arrLang = {
        'tr': {
            '0': 'Ana sayfa',
            '1': 'Yönetici',
            '2': 'Film Listesi',
            '3': 'Aksiyon',
            '4': 'Drama',
            '5': 'Bilim Kurgu',
            '6': 'Komedi',
            '7': 'Film Açıklama',
            '8': 'Yapımcılar ve Oyuncular',
            '9': 'Film Süresi',
            '10': 'Yorumlar',
            '11': 'Gönder',
            '12': 'Yönetmen',
            '13': 'Oyuncular',
        },
        'en': {
            '0': 'Home',
            '1': 'Administrator',
            '2': 'Movies List',
            '3': 'Action',
            '4': 'Drama',
            '5': 'Science Fiction',
            '6': 'Comedy',
            '7': 'Movie Description',
            '8': 'Cast',
            '9': 'Time',
            '10': 'Comments',
            '11': 'Send',
            '12': 'Director',
            '13': 'Actors',
        },
    };
    $('.dil-item').click(function () {
        localStorage.setItem('dil', JSON.stringify($(this).attr('id')));
        location.reload();
    });

    var lang = JSON.parse(localStorage.getItem('dil'));

    if (lang == "tr") {
        $("#drop_yazı").html("Türkçe");
    }
    else {
        $("#drop_yazı").html("English");

    }

    $('a,h5,p,h1,h2,span,li,button,h3,h4,label').each(function (index, element) {
        $(this).text(arrLang[lang][$(this).attr('key')]);

    });

});
