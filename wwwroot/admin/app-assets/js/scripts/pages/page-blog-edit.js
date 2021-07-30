/*=========================================================================================
    File Name: blog-edit.js
    Description: Blog edit field select2 and quill editor JS
    ----------------------------------------------------------------------------------------
    Item Name: Vuexy  - Vuejs, HTML & Laravel Admin Dashboard Template
    Author: PIXINVENT
  Author URL: http://www.themeforest.net/user/pixinvent
==========================================================================================*/
(function (window, document, $) {
    'use strict';

    var select = $('.select2');
    var editor = '#blog-editor-container .editor';
    var blogFeatureImage = $('#blog-feature-image');
    var blogImageText = document.getElementById('blog-image-text');
    var blogImageInput = $('#blogCustomFile');

    // Basic Select2 select
    select.each(function () {
        var $this = $(this);
        $this.wrap('<div class="position-relative"></div>');
        $this.select2({
            // the following code is used to disable x-scrollbar when click in select input and
            // take 100% width in responsive also
            dropdownAutoWidth: true,
            width: '100%',
            dropdownParent: $this.parent()
        });
    });

    // Snow Editor

    var Font = Quill.import('formats/font');
    var Delta = Quill.import('delta');
    Font.whitelist = ['sofia', 'slabo', 'roboto', 'inconsolata', 'ubuntu'];
    Quill.register(Font, true);
    Quill.register("modules/imageUploader", ImageUploader);
    var blogEditor = new Quill(editor, {
        bounds: editor,
        modules: {
            formula: true,
            syntax: true,
            toolbar: {
                container: [
                    [
                        {
                            font: []
                        },
                        {
                            size: []
                        }
                    ],
                    ['bold', 'italic', 'underline', 'strike'],
                    [
                        {
                            color: []
                        },
                        {
                            background: []
                        }
                    ],
                    [
                        {
                            script: 'super'
                        },
                        {
                            script: 'sub'
                        }
                    ],
                    [
                        {
                            header: '1'
                        },
                        {
                            header: '2'
                        },
                        'blockquote',
                        'code-block'
                    ],
                    [
                        {
                            list: 'ordered'
                        },
                        {
                            list: 'bullet'
                        },
                        {
                            indent: '-1'
                        },
                        {
                            indent: '+1'
                        }
                    ],
                    [
                        'direction',
                        {
                            align: []
                        }
                    ],
                    ['link', 'image', 'video', 'formula'],
                    ['clean']
                ]
            },
            imageUploader: {
                upload: file => {
                    return new Promise((resolve, reject) => {
                        const formData = new FormData();
                        formData.append("image", file);

                        fetch(
                            "/Ajax/News/UploadImageNews",
                            {
                                method: "POST",
                                body: formData
                            }
                        )
                            .then(response => response.json())
                            .then(result => {
                                resolve(result.data);
                            })
                            .catch(error => {
                                reject("Upload failed");
                                console.error("Error:", error);
                            });
                    });
                }
            }
        },
        theme: 'snow'
    });
    // Change featured image
    if (blogImageInput.length) {
        $(blogImageInput).on('change', function (e) {
            if ($(this)[0].files.length > 0) {
                var file = $(this)[0].files[0];
                const formData = new FormData();
                formData.append("image", file);
                $.ajax({
                    url: "/Ajax/News/UploadImageNews",
                    type: "POST",
                    timeout: 0,
                    contentType: false,
                    processData: false,
                    data: formData,
                    success: function (result) {
                        blogFeatureImage.attr('src', result.data);
                        featherImage= result.data;
                    },
                    error: function (err) {
                        console.log("Lỗi", err);
                    }
                });
            }
        });
    }
})(window, document, jQuery);
