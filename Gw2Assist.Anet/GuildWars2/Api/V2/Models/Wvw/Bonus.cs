namespace Gw2Assist.Anet.GuildWars2.Api.V2.Models.Wvw
{
    public class Bonus
    {
        /// <summary>
        /// Gets or sets the owner that the bonus belongs to.
        /// </summary>
        public Enums.BorderlandColor Owner { get; set; }

        /// <summary>
        /// Gets or sets the type of bonus.
        /// </summary>
        public Enums.BonusType Type { get; set; }
    }
}
