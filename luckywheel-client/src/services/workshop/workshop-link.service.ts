const WORKSHOP_REGEX_1 = /(https:\/\/)?steamcommunity\.com\/sharedfiles\/filedetails\/\?id=(\w{10})(&searchtext=.*)?/m;
const WORKSHOP_REGEX_2 = /(https:\/\/)?steamcommunity\.com\/workshop\/filedetails\/\?id=(\w{10})(&searchtext=.*)?/m;

function getWorkshopId(link: string): string | undefined {
  let match = WORKSHOP_REGEX_1.exec(link);
  if(!match) {
    match = WORKSHOP_REGEX_2.exec(link);
  }
  return match ? match[2] : undefined;
}

export default {
  getWorkshopId
}
