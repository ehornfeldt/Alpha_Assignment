document.addEventListener('DOMContentLoaded', function () {
    //showModal();
    initWyswyg('#add-project-description-wysiwyg-editor', '#add-project-description-wysiwyg-toolbar', '#add-project-description');
    getEditModals();
    projectMenuSelect();
})

//function showModal () {
//    var showModal = '@ViewData["ShowAddModal"]' === 'True';

//    if (showModal) {
//        const modalElement = document.getElementById('addProjectModal');
//        const modal = new bootstrap.Modal(modalElement);
//        modal.show();
//    }
//}

//Quill.js
function initWyswyg(wysiwygEditorId, wysiwygToolbarId, textareaId) {
    console.log('Initializing WYSIWYG editor...');
    const textarea = document.querySelector(textareaId);
    const content = textarea.value;

    const quill = new Quill(wysiwygEditorId, {
        modules: {
            toolbar: wysiwygToolbarId
        },
        theme: 'snow',
        placeholder: "Type something"
    });

    if (content) {
        quill.root.innerHTML = content;
    }

    quill.on('text-change', function () {
        textarea.value = quill.root.innerHTML;
    });
}

//Fått hjälp av chatGPT med denna
function getEditModals() {
    const editModals = document.querySelectorAll('[id^="editProjectModal"]');

    editModals.forEach(modal => {
        modal.addEventListener('shown.bs.modal', function () {
            const id = modal.dataset.projectId;

            if (!modal.dataset.quillInitialized) {
                const editorId = `#edit-project-description-wysiwyg-editor-${id}`;
                const toolbarId = `#edit-project-description-wysiwyg-toolbar-${id}`;
                const textareaId = `#edit-project-description-${id}`;
                const content = document.querySelector(textareaId)?.value;

                initWyswyg(editorId, toolbarId, textareaId, content);
                modal.dataset.quillInitialized = true;
            }
        });
    });
}

//Fått hjälp av chatgpt med denna
function projectMenuSelect() {
    const buttons = document.querySelectorAll('.project-menu-item');
    const projects = document.querySelectorAll('.project-card');

    buttons.forEach(button => {
        button.addEventListener('click', (e) => {
            e.preventDefault();

            buttons.forEach(b => b.classList.remove('selected'));
            button.classList.add('selected');

            const statusFilter = button.getAttribute('data-filter-status');

            projects.forEach(project => {
                const statusId = project.getAttribute('data-status-id');
                const shouldShow = statusFilter === "" || statusFilter === statusId;
                project.style.display = shouldShow ? "block" : "none";
            });
        });
    });
}

//Denna gjorde jag själv först men valde att använda den från chatGPT istället

//function projectMenuSelect() {
//    document.querySelectorAll('.project-menu-item').forEach(btn => {
//        btn.addEventListener('click', () => {
//            document.querySelectorAll('.project-menu-item').forEach(b => b.classList.remove('selected'));
//            btn.classList.add('selected');
//        });
//    });
//}