// PlexAdmin JavaScript functions

window.downloadFile = function (filename, content) {
    const blob = new Blob([content], { type: 'text/plain;charset=utf-8' });
    const url = window.URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = filename;
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
    window.URL.revokeObjectURL(url);
};

window.triggerDownload = async function (url, filename) {
    try {
        // Fetch the file content without navigation
        const response = await fetch(url);

        if (!response.ok) {
            console.error(`HTTP error! status: ${response.status}`);
            return false;
        }

        // Get the content as a blob
        const blob = await response.blob();

        // Create download link
        const blobUrl = window.URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.style.display = 'none';
        a.href = blobUrl;
        a.download = filename;

        // Trigger download
        document.body.appendChild(a);
        a.click();

        // Cleanup after a delay to ensure download starts
        setTimeout(() => {
            try {
                document.body.removeChild(a);
                window.URL.revokeObjectURL(blobUrl);
            } catch (cleanupError) {
                // Ignore cleanup errors
                console.warn('Cleanup error (safe to ignore):', cleanupError);
            }
        }, 100);

        return true;
    } catch (error) {
        console.error('Download failed:', error);
        // Don't throw - just return false
        return false;
    }
};
