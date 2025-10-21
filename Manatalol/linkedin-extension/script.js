document.addEventListener("DOMContentLoaded", async () => {
     const [tab] = await chrome.tabs.query({ active: true, currentWindow: true });
     const linkedinUrl = tab.url;

    if (!linkedinUrl.includes("linkedin.com/in/")) {
      document.getElementById("sendApi").disabled = true;
    }
    else{
      document.getElementById("sendApi").disabled = false;
    }
});

var btn =   document.getElementById("sendApi");
btn.addEventListener("click", async () => {
    var result = document.getElementById("result");
    btn.textContent= "Sending ...";
    btn.disabled= true;
    const [tab] = await chrome.tabs.query({ active: true, currentWindow: true });
    const linkedinUrl = tab.url;

    if (!linkedinUrl.includes("linkedin.com/in/")) {
      document.getElementById("result").innerText = "Missing Linkedin Url";
      return;
    }

   fetch("https://localhost:7290/api/linkedin", {
      method: "POST",
       headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify({ url: linkedinUrl })
    })
    .then(async (response) => {
      btn.textContent= "Send to ManatalOl";
      btn.disabled= false;
      if (!response.ok) {
        const errorText = await response.text();
        throw new Error(errorText);
      }
      result.innerText = "✓ This profile was successfully added to Manatalol app";
      result.style.color = '#046c04';
    })
    .catch((error) => {
      btn.textContent= "Send to ManatalOl";
      btn.disabled= false;
      result.innerText = JSON.parse(error.message)?.message;
      result.style.color = '#e64242';
  });
});
