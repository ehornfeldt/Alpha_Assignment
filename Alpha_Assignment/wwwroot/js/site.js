document.addEventListener('DOMContentLoaded', function () {
    showModal();
    initWyswyg('#add-project-description-wysiwyg-editor', '#add-project-description-wysiwyg-toolbar', '#add-project-description');
    getEditModals();
    setupProjectFilterMenu();
})

function showModal () {
    var showModal = '@ViewData["ShowAddModal"]' === 'True';

    if (showModal) {
        const modalElement = document.getElementById('addProjectModal');
        const modal = new bootstrap.Modal(modalElement);
        modal.show();
    }
}

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

function getEditModals() {
    //fick här hjälp av chatGPT
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
//hjälp av chatgpt med denna
function setupProjectFilterMenu() {
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

function projectMenuSelect() {
    document.querySelectorAll('.project-menu-item').forEach(btn => {
        btn.addEventListener('click', () => {
            document.querySelectorAll('.project-menu-item').forEach(b => b.classList.remove('selected'));
            btn.classList.add('selected');
        });
    });
}

//document.querySelectorAll('.project-menu-item').forEach(item => {
//    item.addEventListener('click', function () {
//        const selectedItem = document.querySelector('.project-menu-item.selected');
//        if (selectedItem) {
//            selectedItem.classList.remove('selected');
//        }
//        this.classList.add('selected');
//    });
//});


//behövs nedan???

document.querySelectorAll('.form-select').forEach(select => {
    const trigger = select.querySelector('.form-select-trigger');
    const triggrerText = select.querySelector('.form-select-text');
    const options = select.querySelectorAll('.form-select-option');
    const hiddenInput = select.querySelector('input[type="hidden"]');
    const placeholder = select.dataset.placeholder || "Choose";

    const setValue = (value = "", text = placeholder) => {
        triggrerText.textContent = text
        hiddenInput.value = value
        select.classList.toggle('has-placeholder', !value)
    };

    setValue();

    trigger.addEventListener('click', (e) => {
        e.stopPropagation();
        document.querySelectorAll('.form-select.open').forEach(el => el !== select && el.classList.remove('open'));
        select.classList.toggle('open');
    });

    options.forEach(option =>
        option.addEventListener('click', () => {
            setValue(option.dataset.value, option.textContent);
            select.classList.remove('open');
        })
    );

    document.addEventListener('click', e => {
        if (!select.contains(e.target)) {
            select.classList.remove('open');
        }
    });
});
