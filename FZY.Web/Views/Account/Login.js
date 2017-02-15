(function () {

    $(function () {
        $('#LoginButton').click(function (e) {
            e.preventDefault();

            var callback = function (data) {
                location.href = data.targetUrl;
            }
            topevery.ajax({
                url: $("#loginform").attr("action"),
                type: 'POST',
                data: JSON.stringify({
                    usernameOrEmailAddress: $('#EmailAddressInput').val(),
                    password: $('#PasswordInput').val()
                }),
                contentType: "application/json"
            },callback,false);
          
        });

        $('a.social-login-link').click(function () {
            var $a = $(this);
            var $form = $a.closest('form');
            $form.find('input[name=provider]').val($a.attr('data-provider'));
            $form.submit();
        });


        $('#LoginForm input:first-child').focus();
    });

})();