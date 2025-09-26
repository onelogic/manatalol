document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("candidate-form");
    const experienceContainer = document.getElementById("experienceContainer");
    const educationContainer = document.getElementById("educationContainer");
    const addExperienceBtn = document.getElementById("addExperienceBtn");
    const addEducationBtn = document.getElementById("addEducationBtn");
    const skillsHidden = document.getElementById("skills-hidden");
    const skillsTags = document.getElementById("skills-tags");
    const skillInput = document.getElementById("skill-input");

    let skills = [];
    // initialize skills
    if (skillsHidden && skillsHidden.value) {
        skills = skillsHidden.value.split(",").map(s => s.trim()).filter(s => s);
        skills.forEach(v => createSkillTag(v));
    }

    // add skills tags
    function updateSkillsHidden() { if (skillsHidden) skillsHidden.value = skills.join(","); }
    function createSkillTag(value) {
        const tag = document.createElement("span");
        tag.className = "tag bg-primary";
        tag.textContent = value;
        const btn = document.createElement("button");
        btn.type = "button"; btn.textContent = "x";
        btn.addEventListener("click", () => {
            const ix = skills.indexOf(value);
            if (ix > -1) skills.splice(ix, 1);
            tag.remove(); updateSkillsHidden();
        });
        tag.appendChild(btn); skillsTags.appendChild(tag);
    }
    if (skillInput) {
        skillInput.addEventListener("keydown", function (e) {
            if (e.key === "Enter" || e.key === "Tab") {
                e.preventDefault(); e.stopPropagation();
                const v = skillInput.value.trim();
                if (v && !skills.includes(v)) { skills.push(v); createSkillTag(v); updateSkillsHidden(); skillInput.value = ""; }
            }
        });
    }

    // prevent submit when tap enter on input
    if (form) {
        form.addEventListener("keydown", function (e) {
            if (e.key !== "Enter") return;
            const t = e.target;
            if (!t) return;
            if (t.tagName === "TEXTAREA" || t === skillInput) return;
            if (t.type === "submit" || t.type === "button" || t.type === "checkbox") return;
            e.preventDefault();
        });
    }

    // create experiences blocs
    function createExperienceBlockHtml(index) {
        return `
        <div class="border p-3 mb-2 experience-block">
            <div class="row">
                <div class="col-md-6">
                    <label>Company</label>
                    <input name="Candidate.Experiences[${index}].CompanyName" class="form-control mb-2" />
                </div>
                <div class="col-md-6">
                    <label>Position</label>
                    <input name="Candidate.Experiences[${index}].Position" class="form-control mb-2" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <label>Start Date</label>
                    <input type="date" name="Candidate.Experiences[${index}].StartDate" class="form-control mb-2" />
                </div>
                <div class="col-md-6">
                    <div class="row">
                        <div class="col-md-9">
                            <label>End Date</label>
                            <input type="date" name="Candidate.Experiences[${index}].EndDate" class="form-control mb-2" />
                            <div class="invalid-feedback"></div>
                        </div>
                        <div class="col-md-3 align-self-center">
                            <input type="checkbox" class="form-check-input is-current-checkbox" name="Candidate.Experiences[${index}].IsCurrent" value="true" />
                            <label class="form-check-label">Is Current</label>
                        </div>
                    </div>
                </div>
            </div>
            <label>Description</label>
            <textarea name="Candidate.Experiences[${index}].Description" class="form-control mb-2"></textarea>
            <button type="button" class="btn btn-danger btn-sm mt-2 remove-experience">Remove</button>
        </div>`;
    }

    // create educations blocs
    function createEducationBlockHtml(index) {
        return `
        <div class="border p-3 mb-2 education-block">
            <div class="row">
                <div class="col-md-6">
                    <label>School</label>
                    <input name="Candidate.Educations[${index}].School" class="form-control mb-2" />
                </div>
                <div class="col-md-6">
                    <label>Degree</label>
                    <input name="Candidate.Educations[${index}].Degree" class="form-control mb-2" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <label>Start Date</label>
                    <input type="date" name="Candidate.Educations[${index}].StartDate" class="form-control mb-2" />
                </div>
                <div class="col-md-6">
                    <label>End Date</label>
                    <input type="date" name="Candidate.Educations[${index}].EndDate" class="form-control mb-2" />
                    <div class="invalid-feedback"></div>
                </div>
            </div>
            <label>Description</label>
            <textarea name="Candidate.Educations[${index}].Description" class="form-control mb-2"></textarea>
            <button type="button" class="btn btn-danger btn-sm mt-2 remove-education">Remove</button>
        </div>`;
    }

    function reindexCollection(container) {
        Array.from(container.children).forEach((block, i) => {
            block.querySelectorAll('[name]').forEach(el => {
                const old = el.getAttribute('name');
                if (!old) return;
                const newName = old.replace(/\[\d+\]/, `[${i}]`);
                el.setAttribute('name', newName);
            });
        });
    }

    // validation dates
    function validateDatesForBlock(block) {
        const start = block.querySelector("input[type='date'][name$='.StartDate']");
        const end = block.querySelector("input[type='date'][name$='.EndDate']");
        const chk = block.querySelector(".is-current-checkbox");
        const feed = block.querySelector(".invalid-feedback");
        if (!start || !end) return;
        if (chk && chk.checked) { end.classList.remove("is-invalid"); if (feed) feed.innerText = ""; return; }
        if (start.value && end.value && new Date(start.value) > new Date(end.value)) {
            end.classList.add("is-invalid"); if (feed) feed.innerText = "Start Date must be earlier than End Date.";
        } else { end.classList.remove("is-invalid"); if (feed) feed.innerText = ""; }
    }

    //
    function attachListenersToBlock(block) {
        const chk = block.querySelector(".is-current-checkbox");
        const start = block.querySelector("input[type='date'][name$='.StartDate']");
        const end = block.querySelector("input[type='date'][name$='.EndDate']");

        if (chk) {
            chk.addEventListener("change", function () {
                if (end) {
                    if (this.checked) { end.value = ""; end.disabled = true; end.classList.remove("is-invalid"); const f = block.querySelector(".invalid-feedback"); if (f) f.innerText = ""; }
                    else { end.disabled = false; }
                }
            });
            if (end) { end.disabled = !!chk.checked; }
        }

        if (start) start.addEventListener("change", () => validateDatesForBlock(block));
        if (end) {
            end.addEventListener("input", function () { end.classList.remove("is-invalid"); const f = block.querySelector(".invalid-feedback"); if (f) f.innerText = ""; });
            end.addEventListener("change", () => validateDatesForBlock(block));
        }
    }

    document.querySelectorAll(".experience-block").forEach(b => attachListenersToBlock(b));
    document.querySelectorAll(".education-block").forEach(b => attachListenersToBlock(b));

    // add experience
    if (addExperienceBtn) {
        addExperienceBtn.addEventListener("click", function () {
            const index = experienceContainer.children.length;
            const wrapper = document.createElement("div");
            wrapper.innerHTML = createExperienceBlockHtml(index);
            const newBlock = wrapper.firstElementChild;
            experienceContainer.appendChild(newBlock);
            reindexCollection(experienceContainer);
            attachListenersToBlock(newBlock);
        });
    }

    // remove experience
    experienceContainer.addEventListener("click", function (e) {
        if (e.target && e.target.matches(".remove-experience")) {
            const block = e.target.closest(".experience-block");
            if (block) { block.remove(); reindexCollection(experienceContainer); }
        }
    });

    // add education
    if (addEducationBtn) {
        addEducationBtn.addEventListener("click", function () {
            const index = educationContainer.children.length;
            const wrapper = document.createElement("div");
            wrapper.innerHTML = createEducationBlockHtml(index);
            const newBlock = wrapper.firstElementChild;
            educationContainer.appendChild(newBlock);
            reindexCollection(educationContainer);
            attachListenersToBlock(newBlock);
        });
    }

    // remove education
    educationContainer.addEventListener("click", function (e) {
        if (e.target && e.target.matches(".remove-education")) {
            const block = e.target.closest(".education-block");
            if (block) { block.remove(); reindexCollection(educationContainer); }
        }
    });

    form.addEventListener("submit", function (e) {
        reindexCollection(experienceContainer);
        reindexCollection(educationContainer);

        // For each experience block, ensure exactly one field is sent for .IsCurrent:
        // - if checkbox is checked => keep checkbox name, remove any hidden with same name
        // - if checkbox is not checked => remove name from checkbox and create/update hidden name=value false
        Array.from(experienceContainer.children).forEach((block, idx) => {
            const name = `Candidate.Experiences[${idx}].IsCurrent`;
            const chk = block.querySelector('.is-current-checkbox');

            if (chk) {
                if (chk.checked) {
                    chk.setAttribute('name', name);
                    // remove hidden duplicates inside this block
                    const hs = block.querySelectorAll(`input[type="hidden"][name="${name}"]`);
                    hs.forEach(h => h.remove());
                } else {
                    chk.removeAttribute('name');
                    let hidden = block.querySelector(`input[type="hidden"][name="${name}"]`);
                    if (!hidden) {
                        hidden = document.createElement('input');
                        hidden.type = 'hidden';
                        hidden.name = name;
                        hidden.value = 'false';
                        block.appendChild(hidden);
                    } else {
                        hidden.value = 'false';
                    }
                }
            } else {
                let hidden = block.querySelector(`input[type="hidden"][name="${name}"]`);
                if (!hidden) {
                    hidden = document.createElement('input');
                    hidden.type = 'hidden';
                    hidden.name = name;
                    hidden.value = 'false';
                    block.appendChild(hidden);
                } else {
                    hidden.value = 'false';
                }
            }
        });
        document.querySelectorAll('.experience-block').forEach(b => validateDatesForBlock(b));
        document.querySelectorAll('.education-block').forEach(b => validateDatesForBlock(b));
        if (form.querySelectorAll('.is-invalid').length > 0) {
            e.preventDefault();
            const first = form.querySelector('.is-invalid');
            if (first) first.scrollIntoView({ behavior: 'smooth', block: 'center' });
        }
    });
});