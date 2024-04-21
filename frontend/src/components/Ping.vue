<script>
import axios from 'axios'

export default {
  name: 'PingRequest',
  data() {
    return {
      msg: '',
    };
  },
  methods: {
    getMessage() {
      const path = 'http://localhost:5000/test';
      axios.get(path)
        .then((res) => {
          console.log(res);
          this.msg = res.data;
        })
        .catch((error) => {

          console.error(error);
        });
    },
  },
  created() {
    this.getMessage();
  },
};
</script>