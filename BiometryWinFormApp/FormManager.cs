namespace BiometryWinFormApp {
    public class FormManager {
        private Form _startForm;
        private Queue<Form> _formQueue;

        public static FormManager Instance { get; private set; }

        private void SetStart(Form startForm) {
            _startForm = startForm;

            _formQueue = new Queue<Form>();
            _formQueue.Enqueue(startForm);
        }

        public void ShowNext(Form nextForm) {
            nextForm.Show();

            _formQueue.Peek().Hide();

            _formQueue.Enqueue(nextForm);
        }

        public void ShowStart() {
            _startForm.Show();

            /*while (_formQueue.Count > 1)
                _formQueue.Dequeue().Close();*/

            /*if (_formQueue.Peek() != _startForm)
                _formQueue.Dequeue().Close();*/
        }
         
        public void CloseAll() {
            //_startForm?.Dispose();
            _startForm?.Close();
            //Environment.Exit(0);
            Application.Exit();
        }

        public FormManager(Form startForm) {
            if (Instance != null)
                return;

            Instance = this;
            SetStart(startForm);
        }
    }
}
